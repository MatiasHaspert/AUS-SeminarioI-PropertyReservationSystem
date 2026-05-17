import { publicApi } from "@/shared/api/axios";

const AMENITY_URL = "/Amenity";

export const getAmenities = async () => {
    const response = await publicApi.get(AMENITY_URL);
    return response.data;
}
