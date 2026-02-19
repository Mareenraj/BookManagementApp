export interface Book {
  id: number;
  title: string;
  author: string;
  isbn: string;
  publicationDate: string;
}

export type CreateBookDto = Omit<Book, 'id'>;
export type UpdateBookDto = Omit<Book, 'id'>;
