import { Link, useNavigate } from "react-router-dom";
import { useAuth } from "../../context/AuthContext";

const Navbar = () => {
  const { user, logoutUser } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logoutUser();
    navigate("/login");
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark px-3">
      <Link className="navbar-brand" to="/">
        ShramikConnect
      </Link>

      <button
        className="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#mainNavbar"
      >
        <span className="navbar-toggler-icon" />
      </button>

      <div className="collapse navbar-collapse" id="mainNavbar">
        <ul className="navbar-nav me-auto">
          <li className="nav-item">
            <Link className="nav-link" to="/jobs">
              Jobs
            </Link>
          </li>

          {user && user.role === "Worker" && (
            <li className="nav-item">
              <Link className="nav-link" to="/worker/dashboard">
                Worker Dashboard
              </Link>
            </li>
          )}

          {user && user.role === "Client" && (
            <li className="nav-item">
              <Link className="nav-link" to="/client/dashboard">
                Client Dashboard
              </Link>
            </li>
          )}

          {user && user.role === "Organization" && (
            <li className="nav-item">
              <Link className="nav-link" to="/organization/dashboard">
                Organization Dashboard
              </Link>
            </li>
          )}

          {user && user.role === "Admin" && (
            <li className="nav-item">
              <Link className="nav-link" to="/admin/dashboard">
                Admin
              </Link>
            </li>
          )}
        </ul>

        <ul className="navbar-nav ms-auto">
          {!user ? (
            <>
              <li className="nav-item">
                <Link className="nav-link" to="/login">
                  Login
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/register">
                  Register
                </Link>
              </li>
            </>
          ) : (
            <>
              <li className="nav-item text-light d-flex align-items-center me-3">
                {user.email}
              </li>
              <li className="nav-item">
                <button
                  className="btn btn-outline-light btn-sm"
                  onClick={handleLogout}
                >
                  Logout
                </button>
              </li>
            </>
          )}
        </ul>
      </div>
    </nav>
  );
};

export default Navbar;
