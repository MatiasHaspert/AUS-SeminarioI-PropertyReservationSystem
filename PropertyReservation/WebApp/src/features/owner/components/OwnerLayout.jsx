import { useCallback, useEffect, useState } from "react";
import { NavLink, Outlet } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
    faChartLine,
    faHouse,
    faCalendarCheck,
    faFileInvoiceDollar
} from "@fortawesome/free-solid-svg-icons";
import { getPaymentsUnderReview } from "@/features/payment/services/paymentService";

export default function OwnerLayout() {
    const [pendingPayments, setPendingPayments] = useState(0);

    const refreshPendingPayments = useCallback(async () => {
        try {
            const data = await getPaymentsUnderReview();
            setPendingPayments(data?.length || 0);
        } catch {
            setPendingPayments(0);
        }
    }, []);

    useEffect(() => {
        refreshPendingPayments();
    }, [refreshPendingPayments]);

    const linkClass = ({ isActive }) =>
        `owner-sidebar-link${isActive ? " is-active" : ""}`;

    return (
        <div className="owner-shell">
            <aside className="owner-sidebar">
                <div className="owner-sidebar-header">
                    <span className="owner-sidebar-brand-dot">◆</span>
                    <span className="owner-sidebar-brand">Panel</span>
                </div>
                <nav className="owner-sidebar-nav">
                    <NavLink to="/owner" end className={linkClass}>
                        <FontAwesomeIcon icon={faChartLine} />
                        <span>Resumen</span>
                    </NavLink>
                    <NavLink to="/owner/properties" className={linkClass}>
                        <FontAwesomeIcon icon={faHouse} />
                        <span>Propiedades</span>
                    </NavLink>
                    <NavLink to="/owner/reservations" className={linkClass}>
                        <FontAwesomeIcon icon={faCalendarCheck} />
                        <span>Reservas</span>
                    </NavLink>
                    <NavLink to="/owner/payments" className={linkClass}>
                        <FontAwesomeIcon icon={faFileInvoiceDollar} />
                        <span>Pagos</span>
                        {pendingPayments > 0 && (
                            <span className="owner-sidebar-badge">{pendingPayments}</span>
                        )}
                    </NavLink>
                </nav>
            </aside>
            <main className="owner-content">
                <Outlet context={{ refreshPendingPayments, pendingPayments }} />
            </main>
        </div>
    );
}
