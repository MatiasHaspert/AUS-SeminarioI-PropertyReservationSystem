import { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
    faCheckCircle,
    faTimesCircle,
    faExclamationTriangle,
    faInfoCircle,
    faTimes,
} from "@fortawesome/free-solid-svg-icons";

const CONFIG = {
    success: { icon: faCheckCircle,        label: "Éxito"      },
    error:   { icon: faTimesCircle,        label: "Error"      },
    warning: { icon: faExclamationTriangle, label: "Atención"  },
    info:    { icon: faInfoCircle,         label: "Info"       },
};

export default function ToastItem({ toast, onDismiss }) {
    const [visible, setVisible] = useState(false);
    const [leaving, setLeaving] = useState(false);

    useEffect(() => {
        const enter = requestAnimationFrame(() => setVisible(true));
        const leave = setTimeout(() => handleDismiss(), 3800);
        return () => { cancelAnimationFrame(enter); clearTimeout(leave); };
    }, []);

    const handleDismiss = () => {
        setLeaving(true);
        setTimeout(() => onDismiss(toast.id), 350);
    };

    const { icon, label } = CONFIG[toast.type] || CONFIG.info;

    return (
        <div
            className={`toast-item toast-${toast.type} ${visible ? "toast-enter" : ""} ${leaving ? "toast-leave" : ""}`}
            role="alert"
            aria-live="polite"
        >
            <span className="toast-icon">
                <FontAwesomeIcon icon={icon} />
            </span>
            <div className="toast-body">
                <span className="toast-label">{label}</span>
                <span className="toast-message">{toast.message}</span>
            </div>
            <button
                className="toast-close"
                onClick={handleDismiss}
                aria-label="Cerrar notificación"
            >
                <FontAwesomeIcon icon={faTimes} />
            </button>
            <span className="toast-progress" />
        </div>
    );
}
