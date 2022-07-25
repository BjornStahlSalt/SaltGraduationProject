import * as React from 'react';
import Box from '@mui/material/Box';
import Drawer from '@mui/material/Drawer';
import Button from '@mui/material/Button';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';
import MenuIcon from '@mui/icons-material/Menu';
import Divider from '@mui/material/Divider';
import HomeIcon from '@mui/icons-material/Home';
import BookmarkIcon from '@mui/icons-material/Bookmark';
import './Nav.css';

export default function Nav({ levels, handleTitleClick }) {
    const [state, setState] = React.useState({ level: false, });



    const toggleDrawer = (anchor, open) => (event) => {
        if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
            return;
        }

        setState({ ...state, [anchor]: open });
    };

    const list = (anchor) => (
        <Box>
            <HomeIcon></HomeIcon>
            <BookmarkIcon></BookmarkIcon>
            <h3>
                Levels
            </h3 >
            <Divider />
            <List>
                { levels.map((l) => (
                    <ListItem key={ l.title } disablePadding>
                        <ListItemButton >
                            <ListItemText primary={ l.title } onClick={ () => handleTitleClick(l) } />
                        </ListItemButton>
                    </ListItem>
                )) }
            </List>
        </Box>
    );
    return (
        <div>
            { ['left',].map((anchor) => (
                <React.Fragment class='test' key={ anchor }>
                    <Button startIcon={ <MenuIcon /> } onClick={ toggleDrawer(anchor, true) }></Button>
                    <Drawer
                        anchor={ anchor }
                        open={ state[anchor] }
                        onClose={ toggleDrawer(anchor, false) }
                        PaperProps={ {
                            sx: { width: "16%", bgcolor: 'rgba(0, 0, 0, 0.2)' },
                        } }
                    >
                        { list(anchor) }
                    </Drawer>
                </React.Fragment>
            ))
            }
        </div >
    );
};