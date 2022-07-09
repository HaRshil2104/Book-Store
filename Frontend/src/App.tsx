import React from "react";
import AppRoutes from "./App.routes";
import { ThemeProvider } from "@material-ui/core/styles";
import { theme } from "./utils/theme";
import "./assets/css/style.css";
import Header from "./components/header/index";
import Footer from "./components/footer/index";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { AuthWrapper } from "./context/auth";
import { CartWrapper } from "./context/cart";
import MainNavigation from "./components/MainNavigation";

const App: React.FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <React.Suspense fallback={<></>}>
        <BrowserRouter>
          <AuthWrapper>
            <CartWrapper>
              <div className="wrapper">
                <Header />
                <main>
                  <AppRoutes></AppRoutes>
                </main>
                <Footer />
              </div>
            </CartWrapper>
          </AuthWrapper>
          <ToastContainer />
        </BrowserRouter>
      </React.Suspense>
    </ThemeProvider>
  );
};

export default App;
