import { publicApi, privateApi } from "@/shared/api/axios";

const RESERVATION_URL = "/Reservation";

export const createReservation = async (reservationData) => {
    const response = await privateApi.post(RESERVATION_URL, reservationData);
    return response.data;
}

export const getReservationById = async (reservationId) => {
    const response = await privateApi.get(`${RESERVATION_URL}/${reservationId}`);
    return response.data;
}

// Obtener todas las reservas del usuario actual
export const getMyReservations = async () => {
    const response = await privateApi.get(`${RESERVATION_URL}/my-reservations`);
    return response.data;
};


export const getReservationsBypropertyId = async (propertyId) => {
    const response = await privateApi.get(`${RESERVATION_URL}/by-property/${propertyId}`);
    return response.data;
};

export const changeReservationStatus = async (reservationId, status) => {
    const response = await privateApi.patch(`${RESERVATION_URL}/${reservationId}/status`, { status });
    return response.data;
}

export const getReservationsByOwner = async () => {
    const response = await privateApi.get(`${RESERVATION_URL}/owner`);
    return response.data;
}

/* Metodo para cancelar una reserva 
export const cancelReservation = async (id) => {
    try {
        await privateApi.put(`/reservations/${id}/cancel`);
    } catch (error) {
        throw error;
    }
};
*/