export interface ExtractedData {
  id: string;
  documentId: string;
  content: Record<string, any>;
  extractedAt: Date;
}

export interface ExtractedDataCollection {
  items: ExtractedData[];
  totalCount: number;
}