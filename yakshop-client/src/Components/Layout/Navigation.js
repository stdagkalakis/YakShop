import React from 'react'
import { Icon, Container } from 'semantic-ui-react'
import { Link } from 'react-router-dom';

const Navigation = ({ props }) => {
    return (
        // <Header as='h2' onClick={() => props.push("/")}>
        //     <Icon className="angle left icon button" />
        //     <Header.Content>Home</Header.Content>
        // </Header>
        <Container textAlign='left'>
            <Link to='/' className='ui header ' floated='left'>
                < Icon className="angle left icon button" />
                Home
            </Link >
        </Container>
    )
}

export default Navigation;
