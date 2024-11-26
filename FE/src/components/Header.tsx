import React from 'react'
import { Image, Input, Button, Space } from 'antd'
import { SearchOutlined, HeartOutlined, UserOutlined } from '@ant-design/icons'
import { 
  FaHouse, 
  FaPhoneVolume,
  FaRegHeart,
  FaUser,
  FaCartShopping 
} from "react-icons/fa6"
import '/src/styles/HeaderFooter.css'
import { Link, NavLink } from 'react-router-dom'
import meo from '../../public/meo.png'
import vn from '../../public/Vi.png'

const Header: React.FC = () => {
  return (
    <>
      <div className='banner'>
        <img src='/public/banner1.png' alt='Banner' />
      </div>
      <header className='header'>
        <div className='m-auto w-[1300px] flex'>
          <div className='logo'>
              <img src='/public/logo.png' alt='Logo' />
          </div>
          <div className=''>
            <div className='relative ml-[150px]'>
              <input
                className='p-2 mt-[6px] w-[680px] rounded-tl-[25px] rounded-tr-[25px] rounded-br-[25px] rounded-bl-[5px] text-gray-600 placeholder-gray-400 outline-none'
                type='text'
                placeholder='Tìm kiếm sản phẩm...'
              />

              <button className='absolute top-1/2 mt-[3px] right-0 rounded-full transform -translate-y-1/2 text-[#000] bg-[#a7c957] px-4 py-2'>
                TÌM KIẾM
              </button>
            </div>
          </div>

          <div className='ml-[30px] pr-[30px]'>
            <div className='flex mt-[9px] gap-3'>
              <img className='h-[35px]' src={vn} alt='' />
              <img className='h-[23px] w-[40px] mt-[6px]' src={meo} alt='' />
            </div>
          </div>
          <div className='flex gap-[25px]'>
            <div className='border-l pl-6 mt-[8px]'>
              <FaRegHeart className='text-[35px] text-red-500' />
            </div>
            <div className='border-l pl-6 mt-[8px]'>
              <FaUser className='text-[35px] text-black-500' />
            </div>
            <div className='border-l pl-6 mt-[8px]'>
              <FaCartShopping className='text-[35px] text-blue-500' />
            </div>
          </div>
        </div>
      </header>
      <nav className='nav border-t border-b border-gray-200'>
        <div className='max-w-[1300px] mx-auto flex justify-between items-center'>
          <ul className='nav-list flex items-center gap-8 py-3'>
            <FaHouse className='text-[#20B2AA] text-xl' />
            <li className='nav-item'>
              <NavLink 
                to='/' 
                className={({ isActive }) => `
                  text-[#20B2AA] text-lg font-medium 
                  hover:opacity-80 relative
                  ${isActive ? 'after:content-[""] after:absolute after:-bottom-3 after:left-0 after:w-full after:h-1 after:bg-red-500' : ''}
                `}
              >
                Trang Chủ
              </NavLink>
            </li>
            <li className='nav-item'>
              <NavLink 
                to='/san-pham' 
                className={({ isActive }) => `
                  text-[#FF0000] text-lg font-medium 
                  hover:opacity-80 relative
                  ${isActive ? 'after:content-[""] after:absolute after:-bottom-3 after:left-0 after:w-full after:h-1 after:bg-red-500' : ''}
                `}
              >
                Sản Phẩm
              </NavLink>
            </li>
            <li className='nav-item'>
              <NavLink 
                to='/gioi-thieu' 
                className={({ isActive }) => `
                  text-[#FFA500] text-lg font-medium 
                  hover:opacity-80 relative
                  ${isActive ? 'after:content-[""] after:absolute after:-bottom-3 after:left-0 after:w-full after:h-1 after:bg-red-500' : ''}
                `}
              >
                Giới Thiệu
              </NavLink>
            </li>
            <li className='nav-item'>
              <NavLink 
                to='/lien-he' 
                className={({ isActive }) => `
                  text-[#1E90FF] text-lg font-medium 
                  hover:opacity-80 relative
                  ${isActive ? 'after:content-[""] after:absolute after:-bottom-3 after:left-0 after:w-full after:h-1 after:bg-red-500' : ''}
                `}
              >
                Liên Hệ
              </NavLink>
            </li>
          </ul>
          <div className='hotline flex items-center'>
            <FaPhoneVolume className='text-red-500 text-xl mr-2' />
            <span className='font-medium mr-2'>Hotline:</span>
            <span className='text-red-500 font-bold text-lg'>0988 456 550</span>
          </div>
        </div>
      </nav>
    </>
  )
}

export default Header