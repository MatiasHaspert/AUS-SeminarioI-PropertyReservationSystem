import { publicApi, privateApi } from "@/shared/api/axios";

const PROPERTYIMAGE_URL = "/PropertyImage";

export const getPropertyImages = async (propertyId) => {
    const response = await publicApi.get(`${PROPERTYIMAGE_URL}/${propertyId}`);
    return response.data;
}

export const uploadPropertyImages = async (propertyId, files) => {
    const formData = new FormData();
    files.forEach(file => {
        formData.append('files', file);
    });
    const response = await privateApi.post(`${PROPERTYIMAGE_URL}/${propertyId}`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    });
    return response.data;
}

export const setMainImage = async (id) => {
    const response = await privateApi.put(`${PROPERTYIMAGE_URL}/main/${id}`);
    return response.data;
}

export const deletePropertyImage = async (id) => {
    const response = await privateApi.delete(`${PROPERTYIMAGE_URL}/${id}`);
    return response.data;
}
