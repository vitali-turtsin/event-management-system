import './TitleBanner.css';
import picture from '../../assets/libled.jpg';

const TitleBanner = ({ title = "" }) => {
    return (
        <div className="banner-header">
            <div className="title">{title}</div>
            <div className="image">
                <img src={picture} alt="banner" />
            </div>
        </div>
    );
};

export default TitleBanner;