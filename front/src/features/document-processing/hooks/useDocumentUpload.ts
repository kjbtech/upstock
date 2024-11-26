import { useState, useCallback } from 'react';
import { Document } from '../domain/Document';
import { ExtractedData } from '../domain/ExtractedData';
import { DocumentApiRepository } from '../infrastructure/DocumentApiRepository';
import { useAlert } from '@/shared/hooks/useAlert';

export function useDocumentUpload() {
  const [files, setFiles] = useState<File[]>([]);
  const [isUploading, setIsUploading] = useState(false);
  const [uploadProgress, setUploadProgress] = useState(0);
  const [extractedData, setExtractedData] = useState<ExtractedData[]>([]);
  const { showAlert } = useAlert();

  const documentRepository = new DocumentApiRepository();

  const handleFilesSelected = useCallback((newFiles: File[]) => {
    setFiles((prevFiles) => [...prevFiles, ...newFiles]);
  }, []);

  const handleRemoveFile = useCallback((fileToRemove: File) => {
    setFiles((prevFiles) => prevFiles.filter(file => file !== fileToRemove));
  }, []);

  const handleUpload = async () => {
    if (files.length === 0) return;

    setIsUploading(true);
    setUploadProgress(0);

    try {
      const uploadResult = await documentRepository.uploadDocuments(files);
      
      if (uploadResult.isFailure) {
        throw new Error(uploadResult.error || 'Upload failed');
      }

      const documents = uploadResult.getValue();
      const extractedDataResult = await documentRepository.getExtractedData(
        documents.map(doc => doc.id)
      );

      if (extractedDataResult.isFailure) {
        throw new Error(extractedDataResult.error || 'Failed to get extracted data');
      }

      setExtractedData(extractedDataResult.getValue().items);
      setFiles([]);
      showAlert('success', 'Files uploaded and processed successfully!');
    } catch (error) {
      showAlert('error', error instanceof Error ? error.message : 'An error occurred');
    } finally {
      setIsUploading(false);
      setUploadProgress(0);
    }
  };

  return {
    files,
    isUploading,
    uploadProgress,
    extractedData,
    handleFilesSelected,
    handleRemoveFile,
    handleUpload,
  };
}