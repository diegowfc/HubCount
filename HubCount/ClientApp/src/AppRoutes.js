import { ImportarExcel } from "./components/ImportarExcel";
import { Home } from "./components/Home";

const AppRoutes = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/ImportarExcel',
        element: <ImportarExcel />
    }
];

export default AppRoutes;
