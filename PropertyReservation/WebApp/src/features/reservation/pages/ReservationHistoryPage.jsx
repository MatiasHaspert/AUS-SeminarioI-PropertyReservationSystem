import { useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import {
    getReservationsByOwner,
    changeReservationStatus
} from "@/features/reservation/services/reservationService";
import { useToast } from "@/shared/ui/Toast";
import { useConfirm } from "@/shared/ui/ConfirmDialog";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faCalendarAlt,
    faUser,
    faMoneyBillWave,
    faClock,
    faCheckCircle,
    faTimesCircle,
    faHourglassHalf,
    faFlagCheckered,
    faBan,
    faExclamationTriangle,
    faFilter,
    faSpinner,
    faClipboardList
} from '@fortawesome/free-solid-svg-icons';

const STATUS_MAP = {
    "PendingPayment": { label: "Pendiente de Pago", color: "bg-warning text-dark",     icon: faMoneyBillWave },
    "PaymentUploaded": { label: "Pago en Revisión", color: "bg-info text-white",       icon: faHourglassHalf },
    "Confirmed":       { label: "Confirmada",       color: "bg-success text-white",    icon: faCheckCircle },
    "Rejected":        { label: "Rechazada",        color: "bg-danger text-white",     icon: faTimesCircle },
    "Expired":         { label: "Expirada",         color: "bg-secondary text-white",  icon: faClock },
    "Cancelled":       { label: "Cancelada",        color: "bg-secondary text-white",  icon: faBan },
    "Completed":       { label: "Finalizada",       color: "bg-dark text-white",       icon: faFlagCheckered }
};

const FILTER_OPTIONS = [
    { key: "all",       label: "Todas" },
    { key: "active",    label: "Activas" },
    { key: "Confirmed", label: "Confirmadas" },
    { key: "Completed", label: "Finalizadas" },
    { key: "Rejected",  label: "Rechazadas" },
    { key: "Cancelled", label: "Canceladas" }
];

const ACTIVE_STATUSES = new Set(["PendingPayment", "PaymentUploaded", "Confirmed"]);

const RESERVATION_STATUS_CODE = {
    Completed: 7,
    Cancelled: 6
};

export default function ReservationHistoryPage() {
    const navigate = useNavigate();
    const toast = useToast();
    const confirm = useConfirm();
    const [reservations, setReservations] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [filter, setFilter] = useState("all");
    const [processingId, setProcessingId] = useState(null);

    useEffect(() => {
        loadReservations();
    }, []);

    const loadReservations = async () => {
        try {
            setLoading(true);
            const data = await getReservationsByOwner();
            const sorted = (data || []).sort(
                (a, b) => new Date(b.startDate) - new Date(a.startDate)
            );
            setReservations(sorted);
        } catch (err) {
            console.error("Error al cargar el historial:", err);
            setError("No pudimos cargar el historial de reservas.");
        } finally {
            setLoading(false);
        }
    };

    const handleMarkCompleted = async (reservationId) => {
        const ok = await confirm("Esta acción marcará la estadía como completada.", {
            title: "Finalizar estadía",
            confirmText: "Sí, finalizar",
            variant: "default",
        });
        if (!ok) return;

        setProcessingId(reservationId);
        try {
            await changeReservationStatus(reservationId, RESERVATION_STATUS_CODE.Completed);
            setReservations(prev =>
                prev.map(r => r.id === reservationId ? { ...r, status: "Completed" } : r)
            );
        } catch (err) {
            console.error("Error al marcar como finalizada:", err);
            toast("No se pudo actualizar la reserva.", "error");
        } finally {
            setProcessingId(null);
        }
    };

    const handleCancel = async (reservationId) => {
        const ok = await confirm("Esta acción no se puede deshacer.", {
            title: "Cancelar reserva",
            confirmText: "Sí, cancelar",
            variant: "danger",
        });
        if (!ok) return;

        setProcessingId(reservationId);
        try {
            await changeReservationStatus(reservationId, RESERVATION_STATUS_CODE.Cancelled);
            setReservations(prev =>
                prev.map(r => r.id === reservationId ? { ...r, status: "Cancelled" } : r)
            );
        } catch (err) {
            console.error("Error al cancelar la reserva:", err);
            toast("No se pudo cancelar la reserva.", "error");
        } finally {
            setProcessingId(null);
        }
    };

    const filteredReservations = useMemo(() => {
        if (filter === "all") return reservations;
        if (filter === "active") {
            return reservations.filter(r => ACTIVE_STATUSES.has(r.status));
        }
        return reservations.filter(r => r.status === filter);
    }, [reservations, filter]);

    const counts = useMemo(() => {
        const base = { total: reservations.length, active: 0 };
        for (const r of reservations) {
            if (ACTIVE_STATUSES.has(r.status)) base.active += 1;
            base[r.status] = (base[r.status] || 0) + 1;
        }
        return base;
    }, [reservations]);

    const formatDate = (dateString) => {
        if (!dateString) return "-";
        return new Date(dateString).toLocaleDateString('es-AR', {
            day: 'numeric', month: 'long', year: 'numeric'
        });
    };

    const isPastEndDate = (endDate) => {
        if (!endDate) return false;
        const today = new Date();
        today.setHours(0, 0, 0, 0);
        return new Date(endDate) < today;
    };

    if (loading) return (
        <div className="d-flex justify-content-center mt-5">
            <div className="spinner-border text-success" role="status">
                <span className="visually-hidden">Cargando...</span>
            </div>
        </div>
    );

    return (
        <div className="container mt-4 mb-5">
            <div className="d-flex align-items-center mb-4 border-bottom pb-3">
                <h2 className="mb-0 text-success fw-bold">
                    <FontAwesomeIcon icon={faClipboardList} className="me-2" />
                    Historial de Reservas
                </h2>
            </div>

            {error && (
                <div className="alert alert-danger">
                    <FontAwesomeIcon icon={faExclamationTriangle} className="me-2" /> {error}
                </div>
            )}

            {/* Filtros */}
            <div className="d-flex flex-wrap align-items-center gap-2 mb-4">
                <span className="text-muted me-1">
                    <FontAwesomeIcon icon={faFilter} className="me-1" /> Filtrar:
                </span>
                {FILTER_OPTIONS.map(opt => {
                    const count = opt.key === "all" ? counts.total : (counts[opt.key] || 0);
                    const isActive = filter === opt.key;
                    return (
                        <button
                            key={opt.key}
                            className={`btn btn-sm ${isActive ? 'btn-success' : 'btn-outline-success'}`}
                            onClick={() => setFilter(opt.key)}
                        >
                            {opt.label}
                            <span className={`badge ms-2 ${isActive ? 'bg-light text-success' : 'bg-success text-white'}`}>
                                {count}
                            </span>
                        </button>
                    );
                })}
            </div>

            {filteredReservations.length === 0 ? (
                <div className="text-center py-5 bg-light rounded shadow-sm">
                    <h4 className="text-muted mb-3">
                        {reservations.length === 0
                            ? "Todavía no hay reservas para tus propiedades."
                            : "No hay reservas que coincidan con el filtro seleccionado."}
                    </h4>
                    <p className="text-muted">
                        {reservations.length === 0
                            ? "Cuando los huéspedes reserven tus propiedades, aparecerán aquí."
                            : "Probá con otro filtro para ver más resultados."}
                    </p>
                </div>
            ) : (
                <div className="table-responsive bg-white shadow-sm rounded">
                    <table className="table align-middle mb-0">
                        <thead className="table-light">
                            <tr>
                                <th className="ps-3">Propiedad</th>
                                <th>Huésped</th>
                                <th>Fechas</th>
                                <th>Total</th>
                                <th>Estado</th>
                                <th className="text-end pe-3">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            {filteredReservations.map(res => {
                                const statusConfig = STATUS_MAP[res.status] || {
                                    label: res.status,
                                    color: "bg-secondary text-white",
                                    icon: faClock
                                };
                                const canMarkCompleted = res.status === "Confirmed" && isPastEndDate(res.endDate);
                                const canCancel = res.status === "PendingPayment" || res.status === "Confirmed";

                                return (
                                    <tr key={res.id}>
                                        <td className="ps-3">
                                            <div className="d-flex align-items-center gap-3">
                                                <img
                                                    src={res.propertyImageUrl || 'https://via.placeholder.com/80x60?text=Propiedad'}
                                                    alt={res.propertyTitle}
                                                    style={{ width: "64px", height: "48px", objectFit: "cover", borderRadius: "6px" }}
                                                />
                                                <div>
                                                    <div className="fw-bold text-truncate" style={{ maxWidth: "220px" }}>
                                                        {res.propertyTitle || `Propiedad #${res.propertyId}`}
                                                    </div>
                                                    <small className="text-muted">Reserva #{res.id}</small>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <FontAwesomeIcon icon={faUser} className="text-muted me-2" />
                                            {res.guestName || `Usuario #${res.guestId}`}
                                        </td>
                                        <td>
                                            <div className="small">
                                                <FontAwesomeIcon icon={faCalendarAlt} className="text-muted me-2" />
                                                {formatDate(res.startDate)}
                                            </div>
                                            <div className="small text-muted ms-4">
                                                al {formatDate(res.endDate)}
                                            </div>
                                        </td>
                                        <td>
                                            <span className="fw-bold text-success">
                                                ${res.totalPrice?.toLocaleString() || "0"}
                                            </span>
                                        </td>
                                        <td>
                                            <span className={`badge ${statusConfig.color} px-3 py-2 rounded-pill fw-normal`}>
                                                <FontAwesomeIcon icon={statusConfig.icon} className="me-2" />
                                                {statusConfig.label}
                                            </span>
                                        </td>
                                        <td className="text-end pe-3">
                                            <div className="d-flex justify-content-end gap-2">
                                                <button
                                                    className="btn btn-outline-secondary btn-sm"
                                                    onClick={() => navigate(`/property/${res.propertyId}`)}
                                                    title="Ver propiedad"
                                                >
                                                    Ver propiedad
                                                </button>

                                                {canMarkCompleted && (
                                                    <button
                                                        className="btn btn-dark btn-sm fw-bold"
                                                        onClick={() => handleMarkCompleted(res.id)}
                                                        disabled={processingId === res.id}
                                                        title="Marcar estadía como finalizada"
                                                    >
                                                        {processingId === res.id ? (
                                                            <FontAwesomeIcon icon={faSpinner} spin />
                                                        ) : (
                                                            <>
                                                                <FontAwesomeIcon icon={faFlagCheckered} className="me-1" />
                                                                Finalizar
                                                            </>
                                                        )}
                                                    </button>
                                                )}

                                                {canCancel && (
                                                    <button
                                                        className="btn btn-outline-danger btn-sm"
                                                        onClick={() => handleCancel(res.id)}
                                                        disabled={processingId === res.id}
                                                        title="Cancelar reserva"
                                                    >
                                                        {processingId === res.id ? (
                                                            <FontAwesomeIcon icon={faSpinner} spin />
                                                        ) : (
                                                            <>
                                                                <FontAwesomeIcon icon={faBan} className="me-1" />
                                                                Cancelar
                                                            </>
                                                        )}
                                                    </button>
                                                )}
                                            </div>
                                        </td>
                                    </tr>
                                );
                            })}
                        </tbody>
                    </table>
                </div>
            )}
        </div>
    );
}
