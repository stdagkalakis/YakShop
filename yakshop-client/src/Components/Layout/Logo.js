import React from 'react'
import { Header, Image } from 'semantic-ui-react'
import logo from '../../img/YikYak.png';

export const Logo = () => {
    return (
        <Header as='h1' icon textAlign='center' >
            <Image
                centered
                size='massive'
                src={logo}
            />
            <Header.Content>YakShop</Header.Content>
        </Header >
    )
}

export default Logo;