import React, { Component } from 'react';
import { Card, CardBody, CardHeader, Col, Row, Table } from 'reactstrap';
import ProfessionalApi from '../../services/ProfessionalApi';

class Professional extends Component {

    constructor() {
        super();

        this.state = { professional: {} };
        
        this.getProfessionalHandler = this.getProfessionalHandler.bind(this);
    }

    componentDidMount() {
        document.title = "Professional";        
        this.getProfessionalHandler(this.props.match.params.professionalId);
    }

    getProfessionalHandler = (professionalId) => ProfessionalApi.getProfessional(professionalId, professional => this.setState({ professional: professional }));


    render() {

        return (
            <div className="animated fadeIn">
                <Row>
                    <Col lg={6}>
                        <Card>
                            <CardHeader>
                                <strong><i className="icon-info pr-1"></i>Professional id: {this.props.match.params.professionalId}</strong>
                            </CardHeader>
                            <CardBody>
                                <Table responsive striped hover>
                                    <tbody>
                                        
                                        <tr>
                                            <td>{`ID:`}</td>
                                            <td><strong>{this.state.professional.professionalId}</strong></td>
                                        </tr>
                                        <tr>
                                            <td>{`Name:`}</td>
                                            <td><strong>{this.state.professional.firstName}</strong></td>
                                        </tr>
                                        {/* {
                                            patientsTemplate.map(prop =>                                                
                                                <tr key={prop.name}>
                                                    <td>{`${prop.display || utils.capitalize(prop.name)}:`}</td>
                                                    <td><strong>{utils.stringify(this.state.patient[prop.name])}</strong></td>
                                                </tr>
                                            )
                                        } */}
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

export default Professional;
