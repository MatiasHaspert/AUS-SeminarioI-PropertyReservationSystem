import { publicApi, privateApi } from "@/shared/api/axios";

const PROPERTYAVAILABILITY_URL = "/PropertyAvailability";

export const getPropertyAvailabilities = async (propertyId) => {
    const response = await publicApi.get(`${PROPERTYAVAILABILITY_URL}/${propertyId}`);
    return response.data;
}

export const createPropertyAvailability = async (data) => {
    const response = await privateApi.post(PROPERTYAVAILABILITY_URL, data);
    return response.data;
}

export const updatePropertyAvailability = async (id, data) => {
    const response = await privateApi.put(`${PROPERTYAVAILABILITY_URL}/${id}`, data);
    return response.data;
}

export const deletePropertyAvailability = async (id) => {
    const response = await privateApi.delete(`${PROPERTYAVAILABILITY_URL}/${id}`);
    return response.data;
}
