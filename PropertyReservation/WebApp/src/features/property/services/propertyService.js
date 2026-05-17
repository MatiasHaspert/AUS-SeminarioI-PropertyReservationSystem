import { publicApi, privateApi } from "@/shared/api/axios";

const PROPERTY_URL = "/Property";

export const getProperties = async () => {
    const response = await publicApi.get(PROPERTY_URL);
    return response.data;
}

export const getPropertyDetails = async (id) => {
    const response = await publicApi.get(`${PROPERTY_URL}/${id}`);
    return response.data;
}

export async function searchProperties(filters) {
    const response = await publicApi.get(`${PROPERTY_URL}/search`, { params: filters });
    return response.data;
}

export const getPropertiesByOwner = async () => {
    const response = await privateApi.get(`${PROPERTY_URL}/my`);
    return response.data;
}

export const createProperty = async (propertyData) => {
    const response = await privateApi.post(PROPERTY_URL, propertyData);
    return response.data;
}

export const updateProperty = async (id, propertyData) => {
    const response = await privateApi.put(`${PROPERTY_URL}/${id}`, propertyData);
    return response.data;
}

export const deleteProperty = async (id) => {
    const response = await privateApi.delete(`${PROPERTY_URL}/${id}`);
    return response.data;
}