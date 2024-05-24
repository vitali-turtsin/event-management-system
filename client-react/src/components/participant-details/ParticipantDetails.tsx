import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import TitleBanner from '../title-banner/TitleBanner';
import PersonService from '../../services/PersonService';
import { IParticipant } from '../../models/IParticipant';
import OrganizationService from '../../services/OrganizationService';
import { IPaymentMethod } from '../../models/IPaymentMethod';
import PaymentMethodService from '../../services/PaymentMethodService';
import { IPerson } from '../../models/IPerson';

const ParticipantDetails = () => {
    const { id } = useParams();

    const isOrganization = window.location.href.includes("isOrganization=true");

    const [participant, setParticipant] = useState<IParticipant | null>(null);
    const [paymentMethods, setPaymentMethods] = useState<IPaymentMethod[]>([]);

    useEffect(() => {
        loadParticipant();
    });

    useEffect(() => {
        loadPaymentMethods();
    }, []);

    const navigate = useNavigate();

    const loadParticipant = () => {
        if (isOrganization) {
            OrganizationService
                .get(id!)
                .then((organization) => {
                    if (organization.data) {
                        setParticipant({
                            id: id,
                            isOrganization: true,
                            firstName: organization.data.name,
                            lastName: "",
                            personalCode: organization.data.registrationNumber,
                            paymentMethodId: organization.data.paymentMethodId,
                            numberOfParticipants: organization.data.numberOfParticipants,
                            description: organization.data.description || "",
                            eventId: organization.data.eventId,
                        });
                    }
                });
        } else {
            PersonService.get(id!).then((person) => {
                if (person.data) {
                    setParticipant({
                        id: id,
                        isOrganization: false,
                        firstName: person.data.firstName,
                        lastName: person.data.lastName,
                        personalCode: person.data.personalCode,
                        paymentMethodId: person.data.paymentMethodId,
                        numberOfParticipants: 1,
                        description: person.data.description || "",
                        eventId: person.data.eventId,
                    });
                }
            });
        }
    };

    const loadPaymentMethods = () => {
        PaymentMethodService.getAll().then(paymentMethods => {
            setPaymentMethods(paymentMethods.data);
        });
    };

    const onSubmit = (): void => {
        if (!participant) return;

        participant.isOrganization
            ? editOrganization()
            : editPerson();
    };

    const editPerson = () => {
        const person: IPerson = {
            id: id,
            firstName: participant!.firstName,
            lastName: participant!.lastName,
            personalCode: participant!.personalCode,
            eventId: participant!.eventId!,
            paymentMethodId: participant!.paymentMethodId,
            description: participant!.description,
        };

        PersonService.put(person, person.id!)
            .then(person => {
                navigate(`/events/${person.data.eventId}`);
            });
    };

    const editOrganization = () => {
        const organization = {
            id: id,
            name: participant!.firstName,
            description: participant!.description,
            registrationNumber: participant!.personalCode,
            numberOfParticipants: participant!.numberOfParticipants,
            eventId: participant!.eventId!,
            paymentMethodId: participant!.paymentMethodId,
        };

        OrganizationService.put(organization, organization.id!)
            .then(organization => {
                window.open(`/events/${organization.data.eventId}`, "_self");
            });
    };

    if (!participant) {
        navigate("/");
        return null;
    }

    return (
        <div className="form-page-container">
            <TitleBanner title="Participant Details" />
            <div className="form-container">
                <div className="title">Participant Details</div>
                <div className="form">
                    <div className="form-group">
                        <label htmlFor="name">{participant.isOrganization ? "Name" : "First Name"}:</label>
                        <input type="text" id="name" value={participant.firstName} onChange={e => setParticipant({ ...participant, firstName: e.target.value })} />
                    </div>

                    {!participant.isOrganization && (
                        <div className="form-group">
                            <label htmlFor="last-name">Last Name:</label>
                            <input type="text" id="last-name" value={participant.lastName} onChange={e => setParticipant({ ...participant, lastName: e.target.value })} />
                        </div>
                    )}

                    <div className="form-group">
                        <label htmlFor="code">
                            {participant.isOrganization ? "Reg nr" : "Personal Code"}:
                        </label>
                        <input
                            type="text"
                            id="code"
                            value={participant.personalCode}
                            onChange={e =>
                                setParticipant({ ...participant, personalCode: e.target.value })
                            }
                        />
                    </div>

                    {participant.isOrganization && (
                        <div className="form-group">
                            <label htmlFor="numberOfParticipants">Number of comming participants:</label>
                            <input
                                type="number"
                                id="numberOfParticipants"
                                value={participant.numberOfParticipants}
                                onChange={e => setParticipant({ ...participant, numberOfParticipants: parseInt(e.target.value) })}
                            />
                        </div>
                    )}

                    <div className="form-group">
                        <label htmlFor="paymentMethod">Payment Method:</label>
                        <select
                            id="paymentMethod"
                            className="input"
                            value={participant.paymentMethodId}
                            onChange={e => setParticipant({ ...participant, paymentMethodId: e.target.value })}
                        >
                            {paymentMethods.map(paymentMethod => (
                                <option key={paymentMethod.id} value={paymentMethod.id}>
                                    {paymentMethod.name}
                                </option>
                            ))}
                        </select>
                    </div>

                    <div className="form-group">
                        <label htmlFor="description">Description:</label>
                        <textarea
                            id="description"
                            className="input"
                            value={participant.description}
                            onChange={e => setParticipant({ ...participant, description: e.target.value })}
                        />
                    </div>
                </div>
                <div className="buttons">
                    <button
                        className="button secondary"
                        onClick={() => navigate(`/events/${participant?.eventId}`)}
                    >
                        Back
                    </button>
                    <button className="button primary" onClick={onSubmit}>Save</button>
                </div>
            </div>
        </div>
    );
};

export default ParticipantDetails;