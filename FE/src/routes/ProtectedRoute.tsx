// src/routes/ProtectedRoute.tsx
import { Navigate, useLocation } from 'react-router-dom';

interface ProtectedRouteProps {
  children: React.ReactNode;
  allowedRoles: string[];
}

const ProtectedRoute = ({ children, allowedRoles }: ProtectedRouteProps) => {
  const location = useLocation();
  // Giả sử bạn lưu thông tin user trong localStorage hoặc state management
  const user = JSON.parse(localStorage.getItem('user') || '{}');
  
  if (!user || !user.role) {
    // Redirect họ về trang login nếu chưa đăng nhập
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  if (!allowedRoles.includes(user.role)) {
    // Redirect về trang chủ nếu không có quyền
    return <Navigate to="/" replace />;
  }

  return <>{children}</>;
};

export default ProtectedRoute;