import { LoginForm } from "./components/LoginForm";
import { Register } from "./components/Register";
import {ProtectedRoute} from "./components/ProtectedRoute"
import { GetChart } from "./components/GetChart";

const AppRoutes = [
  {
    index: true,
    element: <ProtectedRoute>
      <GetChart />
    </ProtectedRoute>,
  },
  {
    path: "/login",
    element: <LoginForm />,
  },
  {
    path: "/register",
    element: <Register />,
  },
];

export default AppRoutes;
