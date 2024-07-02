// src/apiService.js
import axios from 'axios';

const API_URL = 'http://localhost:5000/cases';

export const getCases = () => axios.get(API_URL);
export const createCase = (caseData) => axios.post(API_URL, caseData);
export const updateCase = (id, caseData) => axios.put(`${API_URL}/${id}`, caseData);
export const deleteCase = (id) => axios.delete(`${API_URL}/${id}`);
