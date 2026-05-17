import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getProperties, deleteProperty } from "@/features/property/services/propertyService";
import PropertyCard from "@/features/property/components/PropertyCard";
import { useConfirm } from "@/shared/ui/ConfirmDialog";

export default function MyPropertiesPage() {
  const navigate = useNavigate();
  const confirm = useConfirm();
  const [properties, setProperties] = useState([]);
  const [errorMessage, setErrorMessage] = useState(false);

  useEffect(() => {
      loadProperties();
  }, []);

  const loadProperties = async () => {
      try {
          const data = await getProperties();
          setProperties(data);
      } catch (error) {
          console.error("Error cargando propiedades:", error);
          setErrorMessage("Error al cargar tus propiedades. Intenta nuevamente más tarde.");
      }
  };

  const handleEdit = (id) => {
      navigate(`/owner/properties/edit/${id}`);
  };

  const handleDelete = async (id) => {
    const ok = await confirm("Esta acción no se puede deshacer.", {
      title: "Eliminar propiedad",
      confirmText: "Sí, eliminar",
      variant: "danger",
    });
    if (!ok) return;
    try {
      await deleteProperty(id);
      setProperties(properties.filter((p) => p.id !== id));
    } catch (error) {
      console.error("Error al eliminar propiedad:", error);
      setErrorMessage("No se pudo eliminar la propiedad. Intenta nuevamente más tarde.");
    }
  };

  if (errorMessage) {
    return <div className="container-fluid mt-4">{{ errorMessage }}</div>;
  }

  return (
    <div className="container-fluid mt-4">
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h3 className="mb-0">Mis propiedades</h3>
        <button
          className="btn btn-primary"
          onClick={() => navigate('/owner/properties/create')}
        >
          + Crear propiedad
        </button>
      </div>

      <div className="d-flex flex-wrap gap-3 justify-content-center">
        {properties.length === 0 ? (
          <p>No tienes propiedades publicadas.</p>
        ) : (
          properties.map((p) => (
            <PropertyCard
              key={p.id}
              property={p}
              showActions={true}
              onDelete={handleDelete}
              onEdit={handleEdit}
            />
          ))
        )}
      </div>
    </div>
  );
}
