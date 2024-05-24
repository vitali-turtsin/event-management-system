import { ToastContainer } from 'react-toastify';
import './App.css';
import { Route, Routes } from 'react-router-dom';
import Header from './components/header/Header';
import Footer from './components/footer/Footer';
import Home from './components/home/Home';
import EventAdd from './components/event-add/EventAdd';
import EventDetails from './components/event-details/EventDetails';
import ParticipantDetails from './components/participant-details/ParticipantDetails';

function App() {
  return (
    <main className="main">
      <div className="content">
        <ToastContainer />
        <Header />
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/add-event" element={<EventAdd />} />
          <Route path="/events/:id" element={<EventDetails />} />
          <Route path="/participants/:id" element={<ParticipantDetails />} />
        </Routes>
        <Footer />
      </div>
    </main>
  );
}

export default App;