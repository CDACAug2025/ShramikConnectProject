import { useState } from "react";
import { login } from "../../services/authService";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";
import { toast } from "react-toastify";
import { emailRegex, passwordRegex } from "../../utils/constants";

const Login = () => {
  const navigate = useNavigate();
  const { loginUser } = useAuth();

  const [form, setForm] = useState({
    email: "",
    password: ""
  });

  const [errors, setErrors] = useState({});
  const [submitted, setSubmitted] = useState(false);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });

    // clear error when user types
    if (errors[e.target.name]) {
      setErrors({ ...errors, [e.target.name]: null });
    }
  };

  const validate = () => {
    const newErrors = {};

    if (!emailRegex.test(form.email)) {
      newErrors.email = "Enter a valid email address";
    }

    if (!passwordRegex.test(form.password)) {
      newErrors.password =
        "Password must be valid (min 8 chars, uppercase, number, special char)";
    }

    setErrors(newErrors);

    if (Object.keys(newErrors).length > 0) {
      toast.error("Please fix the highlighted errors");
      return false;
    }

    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSubmitted(true);

    if (!validate()) return;

    try {
      const res = await login(form);
      loginUser(res.data);

      toast.success("Login successful");

      switch (res.data.role) {
        case "Admin":
          navigate("/admin/dashboard");
          break;
        case "Worker":
          navigate("/worker/dashboard");
          break;
        case "Client":
          navigate("/client/dashboard");
          break;
        case "Organization":
          navigate("/organization/dashboard");
          break;
        default:
          navigate("/");
      }
    } catch (err) {
      toast.error(err.response?.data || "Invalid email or password");
    }
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-xl-8 col-lg-9 col-md-11">

          <div className="card auth-card auth-shadow">
            <div className="row g-0">

              {/* LEFT SIDEBAR */}
              <div className="col-md-5 d-none d-md-flex auth-sidebar flex-column justify-content-center">
                <h4>Welcome Back 👋</h4>
                <p className="mt-3">
                  Login to manage jobs, applications, payments and chats securely.
                </p>
                <ul className="mt-4">
                  <li>🔐 Secure access</li>
                  <li>📊 Role-based dashboard</li>
                  <li>💬 Built-in chat</li>
                  <li>💰 Escrow payments</li>
                </ul>
              </div>

              {/* LOGIN FORM */}
              <div className="col-md-7 auth-form">

                <h3 className="mb-3 text-center">Login</h3>

                <form onSubmit={handleSubmit} noValidate>
                  <input
                    className={`form-control mb-1 ${
                      submitted && errors.email ? "is-invalid" : ""
                    }`}
                    name="email"
                    placeholder="Email"
                    onChange={handleChange}
                  />
                  {submitted && errors.email && (
                    <div className="invalid-feedback">
                      {errors.email}
                    </div>
                  )}

                  <input
                    className={`form-control mb-1 ${
                      submitted && errors.password ? "is-invalid" : ""
                    }`}
                    type="password"
                    name="password"
                    placeholder="Password"
                    onChange={handleChange}
                  />
                  {submitted && errors.password && (
                    <div className="invalid-feedback">
                      {errors.password}
                    </div>
                  )}

                  <button className="btn btn-success w-100 mt-2">
                    Login
                  </button>

                  <div className="text-center mt-3">
                    <span
                      className="text-primary"
                      style={{ cursor: "pointer" }}
                      onClick={() => navigate("/forgot-password")}
                    >
                      Forgot password?
                    </span>
                  </div>

                  <p className="text-center mt-3 mb-0">
                    Don’t have an account?{" "}
                    <span
                      className="text-primary"
                      style={{ cursor: "pointer" }}
                      onClick={() => navigate("/register")}
                    >
                      Register
                    </span>
                  </p>
                </form>
              </div>

            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
