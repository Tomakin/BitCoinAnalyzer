import { authenticationService } from "../services";
import { Navigate } from "react-router-dom";

const ProtectedRoute = ({ children }) => {
  if (authenticationService.currentUserValue) {
    return children;
  } else {
    return <Navigate to="/login" />;
  }
};

export {ProtectedRoute};
