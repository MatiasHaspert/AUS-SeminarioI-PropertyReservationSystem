using Application.DTOs.Admin;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AdminStatsService
    {
        private readonly UserRepository _userRepository;
        private readonly PropertyRepository _propertyRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly PaymentsRepository _paymentsRepository;
        private readonly ReviewRepository _reviewRepository;

        public AdminStatsService(
            UserRepository userRepository,
            PropertyRepository propertyRepository,
            ReservationRepository reservationRepository,
            ReviewRepository reviewRepository,
            PaymentsRepository paymentsRepository)
        {
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _reservationRepository = reservationRepository;
            _reviewRepository = reviewRepository;
            _paymentsRepository = paymentsRepository;
        }

        public async Task<AdminStatsDTO> GetStatsAsync()
        {
            // TODO:
            //  - UsersByRole: agrupar Users por Role.
            //  - TotalProperties: contar sin filtro IsDeleted.
            //  - Total Reviews: contar Reviews.
            //  - ReservationsByStatus: agrupar Reservations por Status.
            //  - PendingPayments: contar Payments con estado UnderReview.
            //  - TotalRevenue: sum(TotalPrice) sobre reservas Completed.
            //  - TopProperties: top 5 propiedades por cantidad de reservas.

            var stats = new AdminStatsDTO
            {
                
                UsersByRole =  await _userRepository.GetUsersGroupedByRolAsync(),
                TotalProperties = await _propertyRepository.GetTotalProperties(),
                TotalReviews = await _reviewRepository.GetTotalReviews(),
                ReservationsByStatus = await _reservationRepository.GetReservationsGroupedByStatusAsync(),
                PendingPayments = await _paymentsRepository.GetPendingPaymentsCountAsync(),
                TotalRevenue = await _reservationRepository.GetTotalRevenueAsync()
            };

            var topProperties = await _propertyRepository.GetTopPropertiesAsync();
            stats.TopProperties = topProperties.Select(p => new TopPropertyDTO
            {
                PropertyId = p.PropertyId,
                Title = p.Title,
                ReservationsCount = p.ReservationsCount
            }).ToList();

            return stats;
        }
    }
}
