import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { faSearch, faMapMarkerAlt, faUsers } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import DateRangePicker from "@/shared/ui/DateRangePicker";
import "./SearchBar.css";

export default function SearchBar() {
  const navigate = useNavigate();
  const [city, setCity] = useState("");
  const [guests, setGuests] = useState(1);
  const [checkIn, setCheckIn] = useState("");
  const [checkOut, setCheckOut] = useState("");
  const [range, setRange] = useState();

  const handleSubmit = (e) => {
    e.preventDefault();
    const params = new URLSearchParams();
    if (city)    params.append("city", city);
    if (guests)  params.append("guests", String(guests));
    if (checkIn) params.append("checkIn", checkIn);
    if (checkOut) params.append("checkOut", checkOut);
    const qs = params.toString();
    navigate(`/search${qs ? `?${qs}` : ""}`);
  };

  const handleRangeSelect = (r) => {
    setRange(r);
    setCheckIn(r?.from ? r.from.toISOString().split("T")[0] : "");
    setCheckOut(r?.to   ? r.to.toISOString().split("T")[0]   : "");
  };

  const handleClearDates = () => {
    setRange(undefined);
    setCheckIn("");
    setCheckOut("");
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="search-bar-pill">
        {/* Destination */}
        <div className="search-field">
          <FontAwesomeIcon icon={faMapMarkerAlt} className="icon" />
          <input
            type="text"
            placeholder="¿A dónde vas?"
            value={city}
            onChange={(e) => setCity(e.target.value)}
          />
        </div>

        <div className="search-bar-divider" />

        {/* Dates */}
        <DateRangePicker
          range={range}
          onRangeChange={handleRangeSelect}
          placeholderStart="Check-in"
          placeholderEnd="Check-out"
          showClearButton={true}
          onClear={handleClearDates}
        />

        <div className="search-bar-divider" />

        {/* Guests */}
        <div className="search-field">
          <FontAwesomeIcon icon={faUsers} className="icon" />
          <input
            type="number"
            min="1"
            placeholder="Huéspedes"
            value={guests}
            onChange={(e) => setGuests(e.target.value ? Number(e.target.value) : "")}
          />
        </div>

        {/* Submit */}
        <button type="submit" className="search-submit-btn">
          <FontAwesomeIcon icon={faSearch} />
          Buscar
        </button>
      </div>
    </form>
  );
}
