import React from 'react'
import { Layout, Typography, Input } from 'antd'
import {
  FaPhone,
  FaGlobe,
  FaFacebookF,
  FaYoutube,
  FaGoogle,
  FaInstagram,
  FaTwitter,
  FaTiktok,
  FaLocationCrosshairs,
  FaHeadset ,
  FaRegEnvelopeOpen 
} from 'react-icons/fa6'
import '/src/styles/HeaderFooter.css'


const { Footer } = Layout
const { Link } = Typography
const { Search } = Input

const CustomFooter: React.FC = () => {
  return (
    <>
      <Footer className='bg-[#2E2E2E] text-white py-8'>
        <div className='max-w-6xl mx-auto flex justify-between items-center mb-2'>
          <div className='flex items-center space-x-4'>
            <img
              src='https://i.pinimg.com/736x/cf/67/f9/cf67f9faef4beff228ce52209f286e88.jpg'
              alt='Logo'
              className='w-16 h-16'
            />
          </div>
          {/* Search Column */}
          <div className='flex flex-col items-center w-[400px] h-[60px] pb-30'>
            <div className="relative w-full">
              <input
                type="text"
                placeholder="Nhập mail của bạn"
                className="w-full h-12 px-4 rounded-full text-black focus:outline-none"
              />
              <button className="absolute right-0 top-0 h-12 px-8 rounded-full bg-[#D0E63A] text-black font-bold flex items-center">
                Đăng ký
                <svg className="w-6 h-6 ml-2" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                  <path d="M21 21L15 15M17 10C17 13.866 13.866 17 10 17C6.13401 17 3 13.866 3 10C3 6.13401 6.13401 3 10 3C13.866 3 17 6.13401 17 10Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"/>
                </svg>
              </button>
            </div>
          </div>

          {/* Social Media Icons */}
          <div className='flex items-center space-x-4'>
            <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
              <FaFacebookF className='text-[#222222] w-6 h-6' />
            </div>
            <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
              <FaYoutube className='text-[#222222] w-6 h-6' />
            </div>
            <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
              <FaGoogle className='text-[#222222] w-6 h-6' />
            </div>
            <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
              <FaInstagram className='text-[#222222] w-6 h-6' />
            </div>
            <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
              <FaTwitter className='text-[#222222] w-6 h-6' />
            </div>
            <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
              <FaTiktok className='text-[#222222] w-6 h-6' />
            </div>
          </div>

          <div className='flex items-center space-x-2'>
            <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center w-[50px] h-[50px]'>
              <FaPhone className='text-[#222222] w-12 h-12' />
            </div>
            <p className='text-white text-lg'>0988.456.550</p>
          </div>
        </div>
        <div className='max-w-6xl mx-auto flex flex-wrap justify-between'>
          <div className='flex flex-col w-1/4'>
            <Link className='font-bold text-[#BBBBBB]'>LIÊN HỆ</Link>
            <div className='flex flex-col space-y-2'>
            <div className='flex items-center space-x-2'>
                <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
                  <FaLocationCrosshairs  className='text-[#222222] w-6 h-6' />
                </div>
                <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                  79C Điện Biên Phủ, P. Đa Kao, Quận 1
                </Link>
              </div>
              <div className='flex items-center space-x-2'>
                <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
                  <FaHeadset  className='text-[#222222] w-6 h-6' />
                </div>
                <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                  0988.456.550
                </Link>
              </div>
              <div className='flex items-center space-x-2'>
                <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
                  <FaRegEnvelopeOpen   className='text-[#222222] w-6 h-6' />
                </div>
                <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                  duy.nb@topbmk.vn
                </Link>
              </div>
              <div className='flex items-center space-x-2'>
                <div className='bg-[#D0E63A] rounded-full p-2 flex items-center justify-center'>
                  <FaGlobe className='text-[#222222] w-6 h-6' />
                </div>
                <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                  topbmk.vn
                </Link>
              </div>
             
            </div>
          </div>

          <div className='flex flex-col w-1/4'>
            <Link className='font-bold text-[#BBBBBB]'>DANH MỤC</Link>
            <div className='flex flex-col space-y-2'>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Trang chủ
              </Link>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Giới thiệu
              </Link>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Sản phẩm
              </Link>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Tin tức
              </Link>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Liên hệ
              </Link>
            </div>
          </div>

          <div className='flex flex-col w-1/4'>
            <Link className='font-bold text-[#BBBBBB]'>ĐIỀU KHOẢN & CHÍNH SÁCH</Link>
            <div className='flex flex-col space-y-2'>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Chính sách giao hàng
              </Link>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Chính sách tích lũy điểm
              </Link>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Điều khoản điều kiện
              </Link>
            </div>
          </div>

          <div className='flex flex-col w-1/4'>
            <Link className='font-bold text-[#BBBBBB]'>HỖ TRỢ KHÁCH HÀNG</Link>
            <div className='flex flex-col space-y-2'>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Chính sách bảo mật
              </Link>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Chính sách bảo hành đổi trả hàng hóa
              </Link>
              <Link className='text-white' style={{ listStyleType: 'disc', paddingLeft: '1em' }}>
                Chính sách thanh toán
              </Link>
            </div>
          </div>
        </div>
      </Footer>

      <div className='bg-[#1B1B1B] text-white py-2 w-full'>
        <p className='text-sm '>Copyright © 2020 - B.M.K. All Rights Reserved</p>
      </div>
    </>
  )
}

export default CustomFooter
