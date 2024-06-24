import axios from 'axios';

const apiClient = axios.create({
  baseURL: 'https://localhost:7170/',
  headers: {
    'Content-Type': 'application/json',
  }
});

export default apiClient;