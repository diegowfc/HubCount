import React from "react";
import { ImportarExcel } from "./components/ImportarExcel";
import ExibirGraficos from "./components/ExibirGraficos";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/ImportarExcel',
        element: <ImportarExcel />
    },
    {
        path: '/ExibirGraficos',
        element: <ExibirGraficos />
    }
];


export default AppRoutes;
