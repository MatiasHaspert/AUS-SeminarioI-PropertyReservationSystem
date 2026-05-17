import { useState, useRef, useEffect } from "react";
import { createPortal } from "react-dom";
import { DayPicker } from "react-day-picker";
import "react-day-picker/dist/style.css";
import { faCalendar } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { es } from "date-fns/locale";
import "./DateRangePicker.css";

export default function DateRangePicker({
    range,
    onRangeChange,
    placeholderStart = "Llegada",
    placeholderEnd = "Salida",
    minDate = null,
    showClearButton = false,
    onClear = null
}) {
    const [calendarOpen, setCalendarOpen] = useState(false);
    const [popupPos, setPopupPos] = useState({ top: 0, left: 0 });
    const popupRef = useRef(null);
    const toggleButtonRef = useRef(null);

    useEffect(() => {
        if (!calendarOpen) return;

        if (toggleButtonRef.current) {
            const rect = toggleButtonRef.current.getBoundingClientRect();
            setPopupPos({ top: rect.bottom + 10, left: rect.left });
        }

        const handleClickOutside = (event) => {
            if (
                popupRef.current &&
                !popupRef.current.contains(event.target) &&
                toggleButtonRef.current &&
                !toggleButtonRef.current.contains(event.target)
            ) {
                setCalendarOpen(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);
        return () => document.removeEventListener('mousedown', handleClickOutside);
    }, [calendarOpen]);

    const handleRangeSelect = (r) => {
        if (onRangeChange) {
            onRangeChange(r);
        }
    };

    const handleClear = () => {
        if (onClear) {
            onClear();
        }
        setCalendarOpen(false);
    };

    return (
        <div className="date-field">
            <div className="search-field" ref={toggleButtonRef} onClick={() => setCalendarOpen(!calendarOpen)} style={{ cursor: 'pointer' }}>
                <FontAwesomeIcon icon={faCalendar} />
                <input
                    type="text"
                    placeholder={placeholderStart}
                    value={range?.from ? range.from.toLocaleDateString('es-ES', { day: '2-digit', month: 'short', year: 'numeric' }) : ''}
                    readOnly
                    style={{ cursor: 'pointer', flex: 1 }}
                />
                <input
                    type="text"
                    placeholder={placeholderEnd}
                    value={range?.to ? range.to.toLocaleDateString('es-ES', { day: '2-digit', month: 'short', year: 'numeric' }) : ''}
                    readOnly
                    style={{ cursor: 'pointer', flex: 1 }}
                />
            </div>
            {calendarOpen && createPortal(
                <div
                    className="calendar-popup"
                    ref={popupRef}
                    style={{ position: 'fixed', top: popupPos.top, left: popupPos.left }}
                >
                    <DayPicker
                        mode="range"
                        selected={range}
                        onSelect={handleRangeSelect}
                        numberOfMonths={1}
                        disabled={minDate ? { before: minDate } : undefined}
                        locale={es}
                    />
                    {showClearButton && (
                        <div className="calendar-actions">
                            <button
                                type="button"
                                className="btn btn-sm btn-outline-secondary"
                                onClick={handleClear}
                            >
                                Limpiar
                            </button>
                        </div>
                    )}
                </div>,
                document.body
            )}
        </div>
    );
}
