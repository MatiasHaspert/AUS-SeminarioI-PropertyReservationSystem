import { useState } from "react";
import { useAuth } from "@/shared/auth/AuthContext";
import { loginUser } from "@/features/auth/services/authService";
import { useNavigate, Link } from "react-router-dom";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const { login } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError("");
    try {
      const data = await loginUser(email, password);
      login(data);
      navigate("/");
    } catch (err) {
      setError(err.message || "Credenciales incorrectas. Intentá de nuevo.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="auth-page">
      <div className="auth-card animate-scale-in">
        <div className="auth-logo">
          <span className="auth-logo-dot">◆</span>
          Seminario I
        </div>

        <h1 className="auth-heading">Bienvenido</h1>
        <p className="auth-subheading">Iniciá sesión para continuar</p>

        {error && (
          <div className="alert alert-danger mb-3" role="alert">
            {error}
          </div>
        )}

        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="form-label" htmlFor="login-email">
              Correo electrónico
            </label>
            <input
              id="login-email"
              type="email"
              className="form-control"
              placeholder="tu@email.com"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
              autoComplete="email"
            />
          </div>

          <div className="mb-4">
            <label className="form-label" htmlFor="login-password">
              Contraseña
            </label>
            <input
              id="login-password"
              type="password"
              className="form-control"
              placeholder="••••••••"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              autoComplete="current-password"
            />
          </div>

          <button
            type="submit"
            className="btn btn-primary w-100"
            disabled={loading}
            style={{ padding: '0.7rem' }}
          >
            {loading ? (
              <span className="spinner-border spinner-border-sm me-2" role="status" />
            ) : null}
            {loading ? "Ingresando..." : "Ingresar"}
          </button>
        </form>

        <hr className="auth-divider" />

        <p className="auth-footer-text">
          ¿No tenés cuenta?{" "}
          <Link to="/register">Registrate gratis</Link>
        </p>
      </div>
    </div>
  );
}
