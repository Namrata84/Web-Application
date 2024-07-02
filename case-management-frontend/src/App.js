// src/App.js
import React, { useState, useEffect } from 'react';
import Box from './Box';
import { getCases, createCase, updateCase, deleteCase } from './apiService';

function App() {
    const [cases, setCases] = useState([]);
    const [dimensions, setDimensions] = useState({ length: 1, width: 1, height: 1 });
    const [name, setName] = useState('');

    useEffect(() => {
        fetchCases();
    }, []);

    const fetchCases = async () => {
        const response = await getCases();
        setCases(response.data);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setDimensions((prev) => ({ ...prev, [name]: parseFloat(value) }));
    };

    const handleCreate = async () => {
        await createCase({ name, ...dimensions, weight: 1 });
        fetchCases();
    };

    const handleUpdate = async (id) => {
        await updateCase(id, { name, ...dimensions, weight: 1 });
        fetchCases();
    };

    const handleDelete = async (id) => {
        await deleteCase(id);
        fetchCases();
    };

    return (
        <div>
            <label>
                Name:
                <input type="text" value={name} onChange={(e) => setName(e.target.value)} /><br></br>
            </label>
            <label>
                Length:
                <input type="number" name="length" value={dimensions.length} onChange={handleChange} /><br></br>
            </label>
            <label>
                Width:
                <input type="number" name="width" value={dimensions.width} onChange={handleChange} /><br></br>
            </label>
            <label>
                Height:
                <input type="number" name="height" value={dimensions.height} onChange={handleChange} /><br></br>
            </label>
            <button onClick={handleCreate}>Create Case</button>
            <Box length={dimensions.length} width={dimensions.width} height={dimensions.height} /><br></br>

            <ul>
                {cases.map((c) => (
                    <li key={c.id}>
                        {c.name} - {c.length}x{c.width}x{c.height}
                        <button onClick={() => handleUpdate(c.id)}>Update</button>
                        <button onClick={() => handleDelete(c.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
