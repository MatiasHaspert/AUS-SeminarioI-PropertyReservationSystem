import { publicApi, privateApi } from "@/shared/api/axios";

const AUTH_URL = "/Auth";

export const loginUser = async (email, password) => {
    try {
        const response = await publicApi.post(`${AUTH_URL}/login`, { email, password });
        return response.data; 
    } catch (error) {
        throw handleAuthError(error);
    }
};

export const registerUser = async (userData) => {
    try {
        const response = await publicApi.post(`${AUTH_URL}/register`, userData);
        return response.data;
    } catch (error) {
        throw handleAuthError(error);
    }
};

export const getUserProfile = async () => {
    try {
        const response = await privateApi.get(`${AUTH_URL}/me`); 
        return response.data;
    } catch (error) {
        throw handleAuthError(error);
    }
};

// Helper para el manejo de errores
const handleAuthError = (error) => {
    let message = "Ha ocurrido un error inesperado";

    if (error.response) {
        message = error.response.data?.message || error.response.data || error.response.statusText;
    } else if (error.request) {
        // La petición se hizo pero no se recibió respuesta (servidor caído)
        message = "No se pudo conectar con el servidor";
    }

    return new Error(message);
};