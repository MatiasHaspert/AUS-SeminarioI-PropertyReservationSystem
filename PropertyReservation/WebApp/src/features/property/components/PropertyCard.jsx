import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faUser,
  faBed,
  faBath,
  faStar,
  faEdit,
  faTrash,
  faLocationDot,
} from "@fortawesome/free-solid-svg-icons";

export default function PropertyCard({ property, showActions = false, onDelete, onEdit }) {
  const {
    title,
    nightlyPrice,
    maxGuests,
    bedrooms,
    bathrooms,
    city,
    state,
    averageRating,
    mainImage,
  } = property;

  const handleCardClick = () => {
    window.location.href = `/property/${property.id}`;
  };

  return (
    <div className="prop-card" onClick={handleCardClick}>
      {/* Action buttons (edit/delete) */}
      {showActions && (
        <div className="prop-card-actions" onClick={(e) => e.stopPropagation()}>
          <button className="icon-button edit" onClick={() => onEdit?.(property.id)}>
            <FontAwesomeIcon icon={faEdit} />
          </button>
          <button className="icon-button delete" onClick={() => onDelete?.(property.id)}>
            <FontAwesomeIcon icon={faTrash} />
          </button>
        </div>
      )}

      {/* Image */}
      <div className="prop-card-image-wrap">
        {mainImage ? (
          <img
            src={mainImage.url}
            className="prop-card-image"
            alt={title}
          />
        ) : (
          <div className="prop-card-placeholder">◆</div>
        )}

        {/* Rating badge */}
        <div className="prop-card-rating">
          <span className="prop-card-rating-star">
            <FontAwesomeIcon icon={faStar} />
          </span>
          {averageRating.toFixed(1)}
        </div>
      </div>

      {/* Body */}
      <div className="prop-card-body">
        <div className="prop-card-location">
          <FontAwesomeIcon icon={faLocationDot} />
          {city}{state ? `, ${state}` : ""}
        </div>
        <h3 className="prop-card-title">{title}</h3>
        <div className="prop-card-meta">
          <span><FontAwesomeIcon icon={faUser} />{maxGuests} huésp.</span>
          <span><FontAwesomeIcon icon={faBed} />{bedrooms} dorm.</span>
          <span><FontAwesomeIcon icon={faBath} />{bathrooms} baños</span>
        </div>
        <div className="prop-card-price">
          <span className="prop-card-price-amount">${nightlyPrice.toLocaleString()}</span>
          <span className="prop-card-price-unit">/ noche</span>
        </div>
      </div>
    </div>
  );
}
