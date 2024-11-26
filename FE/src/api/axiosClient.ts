import axios from "axios";

const axiosClient = axios.create({
  baseURL: "https://localhost:5000/api", 
  headers: {
    "Content-Type": "application/json",
  },
});

axiosClient.interceptors.request.use(
  (config) => {
    // Thêm token hoặc các logic khác trước khi gửi request
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosClient.interceptors.response.use(
  (response) => {
    return response.data;
  },
  (error) => {
    // Xử lý lỗi toàn cục ở đây
    return Promise.reject(error);
  }
);

export default axiosClient;
