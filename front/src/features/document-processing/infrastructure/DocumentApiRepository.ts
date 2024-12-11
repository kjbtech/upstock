import axios, { AxiosInstance } from 'axios';
import { Document } from '../domain/Document';
import { ExtractedData, ExtractedDataCollection } from '../domain/ExtractedData';
import { DocumentRepository } from '../domain/DocumentRepository';
import { Result } from '@/shared/core/Result';
import { API_CONFIG } from '@/shared/config';

export class DocumentApiRepository implements DocumentRepository {
  private api: AxiosInstance;

  constructor() {
    this.api = axios.create({
      baseURL: API_CONFIG.baseUrl,
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
  }

  async uploadDocuments(files: File[]): Promise<Result<Document[]>> {
    try {
      const formData = new FormData();
      files.forEach((file) => {
        formData.append('files', file);
      });

      const response = await this.api.post<{ documents: Document[] }>(
        '/files',
        formData
      );

      return Result.ok(response.data.documents);
    } catch (error) {
      return Result.fail(this.handleError(error));
    }
  }

  async getExtractedData(documentIds: string[]): Promise<Result<ExtractedDataCollection>> {
    try {
      const response = await this.api.get<ExtractedDataCollection>(
        '/items',
        {
          params: { documentIds: documentIds.join(',') },
        }
      );

      return Result.ok(response.data);
    } catch (error) {
      return Result.fail(this.handleError(error));
    }
  }

  private handleError(error: unknown): string {
    if (axios.isAxiosError(error)) {
      return error.response?.data?.message || 'An error occurred while processing your request';
    }
    return 'An unexpected error occurred';
  }
}