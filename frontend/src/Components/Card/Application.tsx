import { DeleteOutline } from '@mui/icons-material';
import { Box, Card, CardContent, CardHeader, CardMedia, IconButton, Typography } from '@mui/material';
import React from 'react'

type Props = {}

const Application = (props: Props) => {
    return (
        <Card sx={{ display: 'flex', alignItems: 'stretch', borderRadius: 2 }}>
            {/* Color bar */}
            <Box
                sx={{
                width: 6,
                backgroundColor: '#34A853',
                }}
            />
            {/* Card content */}
            <CardContent sx={{ display: 'flex', flexDirection: 'column', flex: 1 }}>
                <Box display="flex" alignItems="center" justifyContent="space-between">
                    <Typography gutterBottom variant="h6" component="div">
                    {/* Role */}
                        Software Engineer
                    </Typography>
                    <IconButton aria-label='delete'>
                        <DeleteOutline />
                    </IconButton>
                </Box>
    
                <Box display="flex" alignItems="center">
                    {/* Logo */}
                    <img
                    src="https://img.logo.dev/google.com?token=pk_eNTjlE4YRuCveqVqG7voeA"
                    alt="Google Logo"
                    style={{ width: 20, height: 20, marginRight: 8 }}
                    />
                    {/* Company Name */}
                    <Typography variant="body2" sx={{ color: 'text.secondary' }}>
                        Google
                    </Typography>
                </Box>
            </CardContent>
    </Card>
    );
}

export default Application