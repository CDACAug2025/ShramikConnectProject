import React from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

export default function Home() {
    return (
        <Container fluid className="d-flex align-items-center justify-content-center min-vh-100 bg-light">
            <Row className="w-100">
                <Col md={8} className="mx-auto text-center">
                    <h1 className="display-4 fw-bold mb-4">Welcome to ShramikConnect</h1>
                    <p className="lead text-muted mb-5">
                        Connecting skilled workers with opportunities
                    </p>
                    <div className="d-grid gap-2 d-sm-flex justify-content-sm-center">
                        <Button variant="primary" size="lg" className="px-5">
                            Get Started
                        </Button>
                        <Button variant="outline-secondary" size="lg" className="px-5">
                            Learn More
                        </Button>
                    </div>
                </Col>
            </Row>
        </Container>
    );
}