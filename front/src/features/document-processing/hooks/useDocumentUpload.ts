import { useState, useCallback } from 'react';
import { Document } from '../domain/Document';
import { DocumentApiRepository } from '../infrastructure/DocumentApiRepository';
import { useAlert } from '@/shared/hooks/useAlert';

export function useDocumentUpload() {
  const [files, setFiles] = useState<File[]>([]);
  const [isUploading, setIsUploading] = useState(false);
  const [uploadProgress, setUploadProgress] = useState(0);
  const { showAlert } = useAlert();

  const documentRepository = new DocumentApiRepository();

  const handleFilesSelected = useCallback((newFiles: File[]) => {
    setFiles((prevFiles) => [...prevFiles, ...newFiles]);
  }, []);

  const handleRemoveFile = useCallback((fileToRemove: File) => {
    setFiles((prevFiles) => prevFiles.filter(file => file !== fileToRemove));
  }, []);

  const handleUpload = async () => {
    if (files.length === 0) return { success: false };

    setIsUploading(true);
    setUploadProgress(0);

    try {
      const uploadResult = await documentRepository.uploadDocuments(files);
      
      if (uploadResult.isFailure) {
        throw new Error(uploadResult.error || 'Upload failed');
      }

      setFiles([]);
      showAlert('success', 'Files uploaded and processed successfully!');
      return { success: true };
    } catch (error) {
      showAlert('error', error instanceof Error ? error.message : 'An error occurred');
      return { success: false };
    } finally {
      setIsUploading(false);
      setUploadProgress(0);
    }
  };

  return {
    files,
    isUploading,
    uploadProgress,
    handleFilesSelected,
    handleRemoveFile,
    handleUpload,
  };
}