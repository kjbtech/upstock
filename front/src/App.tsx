import React from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Layout } from '@/shared/components/Layout/Layout';
import { UploadPage } from '@/features/document-processing/pages/UploadPage';
import { ExtractedDataPage } from '@/features/document-processing/pages/ExtractedDataPage';

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<UploadPage />} />
          <Route path="/extracted-data" element={<ExtractedDataPage />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;