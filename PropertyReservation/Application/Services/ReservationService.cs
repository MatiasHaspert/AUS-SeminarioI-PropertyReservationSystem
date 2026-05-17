using AutoMapper;
using Infrastructure.Repositories;
using Application.DTOs.Reservation;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _reservationRepository;
        private readonly PropertyRepository _propertyRepository;
        private readonly ReviewRepository _reviewRepository;
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly CurrentUserService _currentUserService;

        public ReservationService(
            ReservationRepository reservationRepository,
            PropertyRepository propertyRepository,
            UserRepository userRepository,
            ReviewRepository reviewRepository,
            IMapper mapper,
            CurrentUserService currentUserService
        )
        {
            _reservationRepository = reservationRepository;
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _reviewRepository = reviewRepository;
        }

        public async Task<ReservationResponseDTO> CreateReservationAsync(ReservationRequestDTO reservationRequest)
        {
            int userId = _currentUserService.UserId;

            Reservation reservation = _mapper.Map<Reservation>(reservationRequest);
            reservation.GuestId = userId;

            // Cargar propiedad con sus relaciones necesarias
            var property = await _propertyRepository.GetByIdWithAvailabilitiesAndReservationsAsync(reservationRequest.PropertyId);
            if (property is null)
                throw new ArgumentException($"La propiedad con ID {reservationRequest.PropertyId} no existe.");

            // Validar capacidad de huéspedes
            if (reservation.TotalGuests > property.MaxGuests)
                throw new InvalidOperationException("El número de huéspedes excede la capacidad máxima de la propiedad.");

            // Validar disponibilidad y solapamientos
            if (!property.IsAvailableForDateRange(reservationRequest.StartDate, reservationRequest.EndDate) ||
                property.HasConflictingReservation(reservationRequest.StartDate, reservationRequest.EndDate))
                throw new InvalidOperationException("Las fechas seleccionadas no están disponibles.");

            // Calcular precio total
            reservation.TotalPrice = property.CalculateTotalPrice(reservationRequest.StartDate, reservationRequest.EndDate);
            reservation.Status = ReservationStatus.PendingPayment;
            reservation.CreatedAt = DateTime.UtcNow;

            await _reservationRepository.AddAsync(reservation);
            var response = _mapper.Map<ReservationResponseDTO>(reservation);
            return response;
        }

        public async Task<IEnumerable<MyReservationResponseDTO>> GetReservationsByUserIdAsync()
        {
            int userId = _currentUserService.UserId;
            var reservations = await _reservationRepository.GetByUserIdOrderByDateAsync(userId);

            IEnumerable<MyReservationResponseDTO> response = _mapper.Map<IEnumerable<MyReservationResponseDTO>>(reservations);

            // Para cada reserva, chequear si el usuario ya realizó una reseña para esa propiedad
            foreach (var myReservation in response)
            {
                myReservation.HasReview = await _reviewRepository.ExistsByUserIdAndPropertyIdAsync(userId, myReservation.PropertyId);
            }

            return response;
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetReservationsByPropertyIdForOwnerIdAsync(int propertyId)
        {
            var ownerId = _currentUserService.UserId;
            // Validar property
            var propertyExist = await _propertyRepository.ExistsAsync(propertyId);
            if (!propertyExist)
                throw new ArgumentException($"La propiedad con ID {propertyId} no existe.");


            var reservations = await _reservationRepository.GetByPropertyIdForOwnerIdAsync(propertyId, ownerId);
            return _mapper.Map<IEnumerable<ReservationResponseDTO>>(reservations);
        }

        public async Task ChangeStatusAsync(int reservationId, ChangeReservationStatusDTO dto)
        {
            var ownerId = _currentUserService.UserId;

            var reservation =
                await _reservationRepository.GetByIdWithPropertyAsync(reservationId);

            if (reservation == null)
                throw new KeyNotFoundException("Reserva no encontrada");

            if (reservation.Property.OwnerId != ownerId)
                throw new UnauthorizedAccessException(
                    "No sos el owner de esta propiedad");

            if (!Enum.IsDefined(typeof(ReservationStatus), dto.Status))
                throw new InvalidOperationException("Estado inválido");

            switch (dto.Status)
            {
                case ReservationStatus.Rejected:
                    reservation.Reject();
                    break;

                case ReservationStatus.Confirmed:
                    reservation.ConfirmPayment();
                    break;

                case ReservationStatus.Completed:
                    reservation.Completed();
                    break;

                case ReservationStatus.Cancelled:
                    reservation.Cancel();
                    break;

                default:
                    throw new InvalidOperationException(
                        "Estado no permitido para el owner");
            }

            await _reservationRepository.SaveChangesAsync();
        }

        public async Task<ReservationResponseDTO> GetReservationByIdAsync(int reservationId)
        {
            var userId = _currentUserService.UserId;

            // Cargamos la reserva con su propiedad para validar el acceso
            var reservation = await _reservationRepository.GetByIdWithPropertyAsync(reservationId);

            if (reservation == null)
                throw new KeyNotFoundException("Reserva no encontrada");

            // Validar que el usuario sea el huésped o el dueño de la propiedad
            if (reservation.GuestId != userId && reservation.Property.OwnerId != userId)
                throw new UnauthorizedAccessException("No tenés permiso para ver esta reserva");

            // Chequear si el usuario ya realizo una reseña para esta propiedad
            bool hasReview = await _reviewRepository.ExistsByUserIdAndPropertyIdAsync(userId, reservation.PropertyId);

            ReservationResponseDTO response = _mapper.Map<ReservationResponseDTO>(reservation);
            response.HasReview = hasReview;

            return response;
        }

        public async Task<IEnumerable<ReservationResponseDTO>> GetReservationsForOwnerIdAsync()
        {
            var ownerId = _currentUserService.UserId;
            var reservations = await _reservationRepository.GetByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<ReservationResponseDTO>>(reservations);
        }
    }
}
