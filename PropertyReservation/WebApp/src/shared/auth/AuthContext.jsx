import { createContext, useState, useContext, useEffect } from "react";
import { getUserProfile } from "@/features/auth/services/authService";

const AuthContext = createContext({});

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(localStorage.getItem("token") || null);
    const [errorMessage, setErrorMessage] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        // Definimos la funcion dentro para evitar problemas de dependencias
        const verifyUser = async () => {
            try {
                const userData = await getUserProfile();
                if (userData) {
                    setUser(userData);
                } else {
                    setErrorMessage("No se pudo obtener la informacion del usuario")
                }
            } catch (error) {
                console.error("Token invalido o expirado", error);
                setErrorMessage("Tu sesion ha expirado, por favor ingresa nuevamente.");
                logout(); // Si el token falla, limpiamos todo
            } finally {
                setLoading(false);
            }
        };

        if (token) {
            localStorage.setItem("token", token);
            if (!user) {
                verifyUser();
            } else {
                setLoading(false);
            }
        } else {
            setLoading(false);
            setUser(null);
        }
    }, [token]); // Se ejecuta cuando cambia el token

    const login = (data) => {
        // Guardamos expl�citamente aqu� tambi�n por seguridad
        if (data.token) {
            localStorage.setItem("token", data.token);
            setToken(data.token);
        }

        if (data.user) {
            setUser(data.user);
        }
    };

    // Funcion para Salir
    const logout = () => {
        setUser(null);
        setToken(null);
        setErrorMessage(null);
        localStorage.removeItem("token");
    };

    return (
        <AuthContext.Provider value={{ user, token, login, logout, errorMessage, loading }}>
            {children}
        </AuthContext.Provider>
    );
};

// eslint-disable-next-line react-refresh/only-export-components
export const useAuth = () => useContext(AuthContext);