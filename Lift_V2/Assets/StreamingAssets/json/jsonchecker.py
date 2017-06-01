import os, re, json

files = [f for f in os.listdir() if f.endswith('.json') and re.search(r'^[0-9]\.[0-9]', f)]
for file in files:
    try:
        #opening json file
        with open(file) as data_file:
            data = json.load(data_file)
        
        #checking main fields
        if 'agentAttr' in data:
            attributes = data['agentAttr']
        else:
            raise ValueError(file, 'no \'agentAttr\'')
        if 'nodes' in data:
            nodes = data['nodes']
        else:
            raise ValueError(file, 'no \'nodes\'')
        
        #checking attribute values
        for attribute in attributes:
            if attribute not in ['goal', 'patience', 'mood']:
                raise ValueError(file, 'unknown attribute', attribute)
        for attribute in ['goal', 'patience', 'mood']:
            if attribute not in attributes:
                raise KeyError(file, 'missing attribute', attribute)
        
        #checking possible nodes
        node_names = [n['name'] for n in nodes]
        node_goto = []
        node_flag = False
        for node in nodes:
            if 'toNode' in node:
                node_goto.extend(node['toNode'])
            if node['name'] == 'End':
                pass
            elif 'noResponse' not in node:
                print(file, node['name'], 'does not have \'noResponse\'')
            else:
                node_goto.extend([node['noResponse']])
        for node in node_goto:
            if node not in node_names:
                print(file, 'missing node', node)
                node_flag = True
        '''
        for node in node_names:
            if node not in node_goto and node not in ['Start', 'End']:
                print(file, 'cannot reach', node)
                node_flag = True
        '''
        if node_flag:
            pass
        else:
            print(file, 'PASSED')
    except json.JSONDecodeError:
        print('Error Decoding ' + file + '. Check syntax.')
    except ValueError as error:
        print(error.args)
    except KeyError as error:
        print(error.args)