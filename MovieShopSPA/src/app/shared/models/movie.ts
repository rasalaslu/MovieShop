export interface Movie {
    id: number;
    title: string;
    posterUrl: string;
    backdropUrl: string;
    rating: number;
    overview: string;
    tagline: string;
    budget: number;
    revenue: number;
    imdbUrl: string;
    tmdbUrl: string;
    releaseDate: string;
    runTime: number;
    price: number;
    favoritesCount: number;
    casts: Cast[];
    genres: Genre[];
    reviews: any[];
    trailers: Trailer[];
}

export interface Cast {
    id: number;
    name: string;
    gender: string;
    tmdbUrl: string;
    profilePath: string;
    character: string;
}

export interface Genre {
    id: number;
    name: string;
}

export interface Trailer {
    id: number;
    movieId: number;
    trailerUrl: string;
    name: string;
}