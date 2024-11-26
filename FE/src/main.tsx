import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import ProtectedRoute from './routes/ProtectedRoute'
import App from './App'
import './index.css'

// Các page
import Home from './pages/HomePages'
import About from './pages/AboutPage'

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        {/* public routers */}
        {/* <Route path="/login" element={<Login />} /> */}
        {/* User routers */}
        <Route path='/' element={<App />}>        
        </Route>
        {/* Protected user routes */}
        {/* <Route path="profile" element={
            <ProtectedRoute allowedRoles={['user', 'admin']}>
              <Profile />
            </ProtectedRoute>
          } /> */}

        {/* Admin routes */}
        {/* <Route path="/admin" element={
          <ProtectedRoute allowedRoles={['admin']}>
            <AdminLayout />
          </ProtectedRoute>
        }> */}
      </Routes>
    </BrowserRouter>
  </React.StrictMode>
)
