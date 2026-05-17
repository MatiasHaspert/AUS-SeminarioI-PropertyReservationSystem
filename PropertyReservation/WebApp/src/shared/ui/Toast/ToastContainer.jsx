import { createPortal } from "react-dom";
import ToastItem from "./ToastItem";
import "./Toast.css";

export default function ToastContainer({ toasts, onDismiss }) {
    if (!toasts.length) return null;

    return createPortal(
        <div className="toast-stack" role="region" aria-label="Notificaciones">
            {toasts.map(t => (
                <ToastItem key={t.id} toast={t} onDismiss={onDismiss} />
            ))}
        </div>,
        document.body
    );
}
