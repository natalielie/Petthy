import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Button, Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';

import DeleteModal from './DeleteModal';
import ProfessionalApi from '../../services/ProfessionalApi';

function ProfessionalRow(props) {
    const professional = props.professional
    const professionalLink = `/professionals/${professional.professionalId}`

    const getBadge = (status) => {
        return status === 'Active' ? 'success' :
            status === 'Inactive' ? 'secondary' :
                status === 'Pending' ? 'warning' :
                    status === 'Banned' ? 'danger' :
                        'primary'
    }

    if (professional.professionalRoleId == 1) {
        return (
        <tr key={professional.professionalId.toString()}>
            <th scope="row"><Link to={professionalLink}>{professional.professionalId}</Link></th>
                <td><Link to={professionalLink}>{professional.firstName}</Link> <Link to={professionalLink}>{professional.lastName}</Link></td>
            <td>Veterinerian</td>
            <td>
                <Link to={"/professionals/edit/" + professional.professionalId} params={{ professional: professional }}>
                    <Button block color="info" size="sm">Edit</Button>
                </Link>
            </td>
            <td>
                <DeleteModal onDelete={() => props.deleteProfessionalHandler(professional.professionalId)} />
            </td>
            </tr>
        )
    } else {
        return (
             <tr key={professional.professionalId.toString()}>
             <th scope="row"><Link to={professionalLink}>{professional.professionalId}</Link></th>
                <td><Link to={professionalLink}>{professional.firstName}</Link> <Link to={professionalLink}>{professional.lastName}</Link></td>
            <td>Specialist</td>
            <td>
                <Link to={"/professionals/edit/" + professional.professionalId} params={{ professional: professional }}>
                    <Button block color="info" size="sm">Edit</Button>
                </Link>
            </td>
            <td>
                <DeleteModal onDelete={() => props.deleteProfessionalHandler(professional.professionalId)} />
            </td>
            </tr>
            )
        }
    
}

class Professionals extends Component {

    constructor() {
        super();

        this.state = { professionals: [] };
    }

    componentDidMount() {
        document.title = "Professionals";
        this.updateProfessionalsHandler();
    }

    updateProfessionalsHandler = () => ProfessionalApi.getProfessionals(professionals => this.setState({ professionals: professionals }));

    addProfessionalsHandler = (professional) => ProfessionalApi.addProfessional(professional, this.updateProfessionalsHandler);

    editProfessionalsHandler = (professional) => ProfessionalApi.editProfessional(professional, this.updateProfessionalsHandler);

    deleteProfessionalsHandler = (professionalId) => ProfessionalApi.deleteProfessional(professionalId, this.updateProfessionalsHandler);


    render() {
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xl={8}>
                        <Card>
                            <CardHeader>
                                <i className="fa fa-align-justify"></i> Professionals
                            </CardHeader>
                            <CardBody>
                                <Table responsive hover>
                                    <thead>
                                        <tr>
                                            <th scope="col">Id</th>
                                            <th scope="col">Name</th>
                                            <th scope="col">Professional Role</th>
                                            <th scope="col">Edit</th>
                                            <th scope="col">Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.professionals.map((professional, index) =>
                                            <ProfessionalRow key={index} professional={professional} deleteProfessionalsHandler={this.deleteProfessionalsHandler} />
                                        )}
                                    </tbody>
                                </Table>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}



export default Professionals;
