import { useState } from "react";
import { login } from "../../services/authService";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

const Login = () => {
  const navigate = useNavigate();
  const { loginUser } = useAuth();

  const [form, setForm] = useState({
    email: "",
    password: ""
  });

  const handleChange = (e) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const handleSubmit = async (e) => {
    e.preventDefault();
    const res = await login(form);
    loginUser(res.data);

    // redirect based on role
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
  };

  return (
    <div className="container mt-5 col-md-4">
      <h3>Login</h3>
      <form onSubmit={handleSubmit}>
        <input className="form-control mb-2" name="email" placeholder="Email" onChange={handleChange} />
        <input className="form-control mb-3" type="password" name="password" placeholder="Password" onChange={handleChange} />
        <button className="btn btn-success w-100">Login</button>
      </form>
    </div>
  );
};

export default Login;
