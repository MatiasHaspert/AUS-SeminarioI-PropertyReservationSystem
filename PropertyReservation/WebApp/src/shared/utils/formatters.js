export const formatPrice = (amount) =>
    `$${(amount ?? 0).toLocaleString("es-AR")}`;

export const formatDateAR = (date, options) =>
    new Date(date).toLocaleDateString("es-AR", options);

export const formatDateShort = (date) =>
    new Date(date).toLocaleDateString("es-AR", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
    });

export const formatDateLong = (date) =>
    new Date(date).toLocaleDateString("es-AR", {
        day: "numeric",
        month: "long",
        year: "numeric",
    });
