import { Routes, Route } from "react-router-dom";
import MainLayout from "../components/common/MainLayout";

import Home from "../pages/common/Home";
import Login from "../pages/auth/Login";
import Register from "../pages/auth/Register";


const AppRoutes = () => {
  return (
    <MainLayout>
      <Routes>
        <Route path="/" element={<Home />} />

        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </MainLayout>
  );
};

export default AppRoutes;
