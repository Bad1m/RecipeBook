export interface Review {
    id: string;
    recipeId: number;
    userName: string;
    comment: string;
    rating: number;
    date: Date;
  }

  export interface ReviewRequest {
    recipeId: number;
    userName: string;
    comment: string;
    rating: number;
    date: Date;
  }