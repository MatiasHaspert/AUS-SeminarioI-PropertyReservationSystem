export const ReservationStatus = Object.freeze({
    PendingPayment: "PendingPayment",
    PaymentUploaded: "PaymentUploaded",
    Confirmed: "Confirmed",
    Rejected: "Rejected",
    Expired: "Expired",
    Cancelled: "Cancelled",
    Completed: "Completed",
});

export const PaymentStatus = Object.freeze({
    Pending: "Pending",
    Approved: "Approved",
    Rejected: "Rejected",
});

export const PaymentMethod = Object.freeze({
    Transfer: "Transfer",
    Cash: "Cash",
});

export const Role = Object.freeze({
    User: "User",
    Owner: "Owner",
    Admin: "Admin",
});
