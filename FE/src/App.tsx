import React from 'react'
import Header from './components/Header'
import Footer from './components/Footer'
import { Outlet } from 'react-router-dom'

const App: React.FC = () => {
  return (
    <div>
      <Header />
      <main style={mainStyle}>
        <Outlet />
      </main>
      <Footer />
    </div>
  )
}

const mainStyle: React.CSSProperties = {
  padding: '2rem',
  minHeight: 'calc(100vh - 200px)'
}

export default App
