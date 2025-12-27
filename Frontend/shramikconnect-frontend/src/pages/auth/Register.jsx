import { useState } from "react";
import { register } from "../../services/authService";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { RoleType } from "../../utils/enums";
import {
  emailRegex,
  passwordRegex,
  phoneRegex,
  nameRegex
} from "../../utils/constants";

import "../../assets/styles/auth.css";

const Register = () => {
  const navigate = useNavigate();

  const [form, setForm] = useState({
    fullName: "",
    email: "",
    phone: "",
    password: "",
    role: RoleType.Worker
  });

  const [errors, setErrors] = useState({});
  const [submitted, setSubmitted] = useState(false);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
    if (errors[e.target.name]) {
      setErrors({ ...errors, [e.target.name]: null });
    }
  };

  const validate = () => {
    const newErrors = {};

    if (!nameRegex.test(form.fullName))
      newErrors.fullName = "Enter valid full name";

    if (!emailRegex.test(form.email))
      newErrors.email = "Enter valid email";

    if (!phoneRegex.test(form.phone))
      newErrors.phone = "Enter valid phone number";

    if (!passwordRegex.test(form.password))
      newErrors.password = "Password must be strong";

    setErrors(newErrors);

    if (Object.keys(newErrors).length > 0) {
      toast.error("Please fix highlighted fields");
      return false;
    }

    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setSubmitted(true);
    if (!validate()) return;

    try {
      await register(form);
      toast.success("Registration successful");
      navigate("/login");
    } catch (err) {
      toast.error(err.response?.data || "Registration failed");
    }
  };

  return (
    <div className="container mt-5">
      <div className="row justify-content-center">
        <div className="col-xl-8 col-lg-9 col-md-11">

          <div className="card shadow-lg auth-card">
            <div className="row g-0">

              {/* INFO SIDEBAR */}
              <div className="col-md-5 auth-sidebar p-4 d-none d-md-block">
                <h4>Join ShramikConnect</h4>
                <p className="mt-3">
                  India’s trusted platform for skilled workers and job providers.
                </p>
                <ul className="mt-4">
                  <li>✔ Verified workers & clients</li>
                  <li>✔ Secure escrow payments</li>
                  <li>✔ Transparent job contracts</li>
                  <li>✔ Real-time chat & dispute handling</li>
                </ul>
              </div>

              {/* FORM */}
              <div className="col-md-7 auth-form">

                <h3 className="mb-3">Create Account</h3>

                <form onSubmit={handleSubmit} noValidate>
                  <input
                    className={`form-control mb-1 ${
                      submitted && errors.fullName ? "is-invalid" : ""
                    }`}
                    name="fullName"
                    placeholder="Full Name"
                    onChange={handleChange}
                  />
                  {submitted && errors.fullName && (
                    <div className="invalid-feedback">{errors.fullName}</div>
                  )}

                  <input
                    className={`form-control mb-1 ${
                      submitted && errors.email ? "is-invalid" : ""
                    }`}
                    name="email"
                    placeholder="Email"
                    onChange={handleChange}
                  />
                  {submitted && errors.email && (
                    <div className="invalid-feedback">{errors.email}</div>
                  )}

                  <input
                    className={`form-control mb-1 ${
                      submitted && errors.phone ? "is-invalid" : ""
                    }`}
                    name="phone"
                    placeholder="Phone"
                    onChange={handleChange}
                  />
                  {submitted && errors.phone && (
                    <div className="invalid-feedback">{errors.phone}</div>
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
                    <div className="invalid-feedback">{errors.password}</div>
                  )}

                  <select
                    className="form-control mb-4"
                    name="role"
                    onChange={(e) =>
                      setForm({ ...form, role: Number(e.target.value) })
                    }
                  >
                    <option value={RoleType.Worker}>Worker</option>
                    <option value={RoleType.Client}>Client</option>
                    <option value={RoleType.Organization}>Organization</option>
                  </select>

                  <button className="btn btn-primary w-100 py-2">
                    Register
                  </button>
                </form>
              </div>

            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Register;
