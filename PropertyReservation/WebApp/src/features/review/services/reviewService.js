import { publicApi, privateApi } from "@/shared/api/axios";

const REVIEW_URL = "/Review";

export const getPropertyReviews = async (propertyId) => {
    const response = await publicApi.get(REVIEW_URL, { params: { propertyId } });
    return response.data;
};

export const getReviewById = async (id) => {
    const response = await publicApi.get(`${REVIEW_URL}/${id}`);
    return response.data;
};

export const createReview = async (reviewData) => {
    const response = await privateApi.post(REVIEW_URL, reviewData);
    return response.data;
};

export const updateReview = async (id, reviewData) => {
    const response = await privateApi.put(`${REVIEW_URL}/${id}`, reviewData);
    return response.data;
};
